/*
 * bootloader.c
 *
 *  Created on: Mar 9, 2026
 *      Author: Şule Nur Demirdaş
 */

#include "bootloader.h"

static UART_Transmit_FuncPtr_t p_UART_Tx_Func = NULL;
int test = 0;
uint8_t commands[NUM_OF_COMMANDS] = {
		GET_HELP,
		GET_VERSION,
		GET_ID,
		READ_MEMORY,
		GO_TO_ADDRESS,
		WRITE_MEMORY,
		ERASE,
		WRITE_PROTECT_UNPROTECT,
		READOUT_PROTECT_UNPROTECT,
		GET_CHECKSUM
};
uint8_t response_get_version[RESPONSE_GET_VERSION_SIZE] = {0};
uint8_t response_get_help[RESPONSE_GET_HELP_SIZE] = {0};
uint8_t response_get_id[RESPONSE_GET_ID_SIZE] = {0};
uint8_t response_read_mem[RESPONSE_READ_MEM_SIZE] = {0};
uint8_t response_go_to_address[1] = {0};
uint8_t response_write_memory[1] = {0};
uint8_t response_erase[1] = {0};
uint8_t response_write_protect_unprotect[1] = {0};
uint8_t response_connect[RESPONSE_CONNECT_SIZE] = {0};

void Bootloader_Init(UART_Transmit_FuncPtr_t p_uart_transmit_func)
{
    p_UART_Tx_Func = p_uart_transmit_func;
}

void bootloader_send_response(uint8_t* response_data, uint32_t size)
{
    if (p_UART_Tx_Func != NULL)
    {
        p_UART_Tx_Func(response_data, size);
    }
}
void processBootloaderCommand(char* buffer)
{
	uint8_t command = buffer[2];

	switch (command) {
		case GET_VERSION:
			handleGetVersion();
			break;
		case GET_HELP:
			handleGetHelp();
			break;
		case GET_ID:
			handleGetID();
			break;
		case READ_MEMORY:
			handleReadMem(buffer);
			break;
		case GO_TO_ADDRESS:
			handleGoToAddress(buffer);
			break;
		case WRITE_MEMORY:
			handleWriteMemory(buffer);
			break;
		case ERASE:
			handleErase(buffer);
			break;
		case WRITE_PROTECT_UNPROTECT:
			handleWriteProtectUnprotect(buffer);
			break;
		case CONNECT:
			handleConnect();
			break;
		default:
			break;
	}

}

void handleGetVersion(void)
{
	if (BOOTLOADER_VERSION > 0 && BOOTLOADER_VERSION <= 255) {
		response_get_version[0] = ACK;
		response_get_version[1] = BOOTLOADER_VERSION;
	} else {
		response_get_version[0] = NACK;
		response_get_version[1] = UNKNOWN;
	}
	bootloader_send_response(response_get_version, RESPONSE_GET_VERSION_SIZE);
}


void handleGetHelp(void)
{
	response_get_help[0] = ACK;
	response_get_help[1] = NUM_OF_COMMANDS;
	response_get_help[2] = BOOTLOADER_VERSION;

	for (int i = 0; i < NUM_OF_COMMANDS; i++)
	{
		response_get_help[i+3] = commands[i];
	}

	bootloader_send_response(response_get_help, RESPONSE_GET_HELP_SIZE);
}

void handleGetID(void)
{
	response_get_id[0] = ACK;
	response_get_id[1] = 1;
	response_get_id[2] = 0x04;
	response_get_id[3] = DEVICE_ID;

	bootloader_send_response(response_get_id, RESPONSE_GET_ID_SIZE);

}

int handleReadMem(char* buffer)
{
	uint8_t addrBytes[4] = {buffer[3],buffer[4],buffer[5],buffer[6]};
	uint8_t crc_received = buffer[7];
	uint8_t length = buffer[8];
	uint8_t compOfLength = buffer[9];

	uint8_t crc_calculated = {0};
	uint32_t address = (((uint32_t)buffer[3] << 24) | ((uint32_t)buffer[4] << 16) | ((uint32_t)buffer[5] << 8)  | ((uint32_t)buffer[6]));
	uint8_t* mem_ptr = (uint8_t*)address;

	crc_calculated = CalculateCRC8(addrBytes, 4);

	if (((crc_received != crc_calculated) || (length != (uint8_t)~compOfLength))) // cast edildi cunku ~int tipinde karsilastirir
	{
		response_read_mem[0] = NACK;
		bootloader_send_response(response_read_mem, 1);
		return -1;
	}

	if( !((address >= FLASH_BASE && address <= FLASH_END ) || (address >= SRAM1_BASE && address <= SRAM2_END )) )
	{
		response_read_mem[0] = NACK;
		bootloader_send_response(response_read_mem, 1);
		return -1;
	}
	response_read_mem[0] = ACK;

	for(int i = 0; i <= length; i++ )
	{
		response_read_mem[i+1] = mem_ptr[i];
	}
	bootloader_send_response(response_read_mem, (length + 1));

	return 1;
}

int handleGoToAddress(char* buffer)
{
	uint8_t addrBytes[4] = {buffer[3],buffer[4],buffer[5],buffer[6]};
	uint8_t crc_received = buffer[7];

	uint8_t crc_calculated = {0};
	uint32_t address = (((uint32_t)buffer[3] << 24) | ((uint32_t)buffer[4] << 16) | ((uint32_t)buffer[5] << 8)  | ((uint32_t)buffer[6]));

	crc_calculated = CalculateCRC8(addrBytes, 4);

	if (crc_received != crc_calculated)
	{
		response_go_to_address[0] = NACK;
		bootloader_send_response(response_go_to_address, 1);
		return -1;
	}

	if( !((address >= FLASH_BASE && address <= FLASH_END ) || (address >= SRAM1_BASE && address <= SRAM2_END )) )
	{
		response_go_to_address[0] = NACK;
		bootloader_send_response(response_go_to_address, 1);
		return -1;
	}
	response_go_to_address[0] = ACK;

	bootloader_send_response(response_go_to_address, 1);

	GoToAddress(address);

	return 1;
}


int handleWriteMemory(char* buffer)
{
	uint8_t offset = 13;
	uint8_t addrBytes[4] = {buffer[3],buffer[4],buffer[5],buffer[6]};
	uint8_t addr_crc_received = buffer[7];
	uint8_t dataLength = buffer[8]+1;
	uint8_t data_crc_received = buffer[offset+dataLength];
	uint8_t received_data_bytes[WRITE_MEM_BLOCK_SIZE] = {0};

	uint8_t data_crc_calculated = {0};
	uint8_t addr_crc_calculated = {0};
	uint32_t address = (((uint32_t)buffer[3] << 24) | ((uint32_t)buffer[4] << 16) | ((uint32_t)buffer[5] << 8)  | ((uint32_t)buffer[6]));

	for(int i = 0; i < dataLength; i++ )
	{
		received_data_bytes[i] = buffer[i+offset];
	}
	uint32_t totalBytes = (((uint32_t)buffer[9] << 24) | ((uint32_t)buffer[10] << 16) | ((uint32_t)buffer[11] << 8)  | ((uint32_t)buffer[12]));

	addr_crc_calculated = CalculateCRC8(addrBytes, 4);
	data_crc_calculated = CalculateCRC8(received_data_bytes, dataLength);

	if (((addr_crc_received != addr_crc_calculated) || (data_crc_received != data_crc_calculated)))
	{
		response_write_memory[0] = NACK;
		bootloader_send_response(response_write_memory, 1);
		return -1;
	}

	if( !((address >= FLASH_BASE && address <= FLASH_END ) || (address >= SRAM1_BASE && address <= SRAM2_END )) )
	{
		response_write_memory[0] = NACK;
		bootloader_send_response(response_write_memory, 1);
		return -1;
	}

    HAL_FLASH_Unlock();

    for(int i = 0; i < dataLength; i++)
    {
        if(HAL_FLASH_Program(FLASH_TYPEPROGRAM_BYTE, address + i, received_data_bytes[i]) != HAL_OK)
        {
            HAL_FLASH_Lock();
            response_write_memory[0] = NACK;
            bootloader_send_response(response_write_memory, 1);
            return -1;
        }
    }

    HAL_FLASH_Lock();

	response_write_memory[0] = ACK;
	bootloader_send_response(response_write_memory, 1);
	return 1;
}


void GoToAddress(uint32_t address)
{
	// deinitilize peripherals
	HAL_DeInit();

	// deinitilize clock
	HAL_RCC_DeInit();

	SysTick->CTRL = 0;
	SysTick->LOAD = 0;
	SysTick->VAL = 0;

	uint32_t msp_value = *(__IO uint32_t*)address;
	uint32_t reset_handler_addr = *(__IO uint32_t*)(address + 4);

    __set_MSP(msp_value);
    void (*AppResetHandler)(void) ;

    AppResetHandler = (void (*)(void))reset_handler_addr;
    AppResetHandler();
}

int handleErase(char* buffer)
{
	uint8_t offset = 4;
	uint8_t rx_numOfSectors = buffer[3];
	uint8_t rx_sectors[MAX_NUM_OF_SECTORS + 1] = {0};
	uint8_t crc_received = buffer[offset + rx_numOfSectors];

	rx_sectors[0] = rx_numOfSectors;

	for(int i = 0; i < rx_numOfSectors; i++)
	{
		rx_sectors[i + 1] = buffer[offset + i];
	}

	uint8_t crc_calculated = CalculateCRC8(rx_sectors, rx_numOfSectors + 1);

	if(crc_received != crc_calculated)
	{
        response_erase[0] = NACK;
        bootloader_send_response(response_erase, 1);
        return -1;
	}

	for(int i = 0; i < rx_numOfSectors; i++)
	{
		if(EraseFlashSectors(rx_sectors[i+1]) == -1)
		{
			response_erase[0] = NACK;
			bootloader_send_response(response_erase, 1);
		}
		response_erase[0] = ACK;
		bootloader_send_response(response_erase, 1);
	}
	return 1;
}

void handleWriteProtectUnprotect(char* buffer)
{
	uint8_t offset = 4;
	uint8_t rx_numOfSectors = buffer[3];
	uint8_t rx_sectors[MAX_NUM_OF_SECTORS + 1] = {0};
	uint8_t crc_received = buffer[offset + rx_numOfSectors];
	uint32_t sectors = 0x00;

	rx_sectors[0] = rx_numOfSectors;

	for(int i = 0; i < rx_numOfSectors; i++)
	{
		rx_sectors[i + 1] = buffer[offset + i];
		sectors |= 1 << (rx_sectors[i + 1]);
	}

	uint8_t crc_calculated = CalculateCRC8(rx_sectors, rx_numOfSectors + 1);

	if(crc_received != crc_calculated)
	{
		response_write_protect_unprotect[0] = NACK;
        bootloader_send_response(response_write_protect_unprotect, 1);
        return;
	}

	uint32_t wrp_mask = (~sectors) & 0x0FFF;

	HAL_FLASH_Unlock();
	HAL_FLASH_OB_Unlock();

	while(__HAL_FLASH_GET_FLAG(FLASH_FLAG_BSY) != RESET) {}

	FLASH->OPTCR &= ~(0x0FFF << 16);
	FLASH->OPTCR |= (wrp_mask << 16);

	response_write_protect_unprotect[0] = ACK;
	bootloader_send_response(response_write_protect_unprotect, 1);

	HAL_FLASH_OB_Launch();
}
void handleConnect(void)
{
	uint32_t wrp_status = ~(FLASH->OPTCR >> 16) & 0xFFF; // 1 -> protected, 0 -> unprotected
	uint8_t high_sectors = wrp_status >> 8;
	uint8_t low_sectors = wrp_status & 0xFF;
	uint32_t readout_status = (FLASH->OPTCR >> 8) & 0xFF;

	response_connect[0] = ACK;
	response_connect[1] = high_sectors;
	response_connect[2] = low_sectors;
	response_connect[3] = readout_status;

	bootloader_send_response(response_connect, RESPONSE_CONNECT_SIZE);
	return;
}

int EraseFlashSectors(uint8_t sector)
{
	HAL_StatusTypeDef status;
	uint32_t SectorError = {0};
	FLASH_EraseInitTypeDef FLASH_Erase_Init;
	FLASH_Erase_Init.VoltageRange = FLASH_VOLTAGE_RANGE_3;

	HAL_FLASH_Unlock();

	if (sector == 0xFF)
	{
		FLASH_Erase_Init.TypeErase = FLASH_TYPEERASE_MASSERASE;
		status = HAL_FLASHEx_Erase(&FLASH_Erase_Init, &SectorError);
	}
	else
	{
		FLASH_Erase_Init.NbSectors = 1;
		FLASH_Erase_Init.TypeErase = FLASH_TYPEERASE_SECTORS;
		FLASH_Erase_Init.Sector = (uint32_t)sector;
		status = HAL_FLASHEx_Erase(&FLASH_Erase_Init, &SectorError);
	}
	HAL_FLASH_Lock();

	if (status == HAL_OK)
	{
		return 1;
	}
	else
	{
		return -1;
	}
}
uint8_t GetSectorNumber(uint32_t address)
{
	if( (address >= ADDR_FLASH_SECTOR_0) && (address < ADDR_FLASH_SECTOR_1)) return 0x00;
	if( (address >= ADDR_FLASH_SECTOR_1) && (address < ADDR_FLASH_SECTOR_2)) return 0x01;
	if( (address >= ADDR_FLASH_SECTOR_2) && (address < ADDR_FLASH_SECTOR_3)) return 0x02;
	if( (address >= ADDR_FLASH_SECTOR_3) && (address < ADDR_FLASH_SECTOR_4)) return 0x03;
	if( (address >= ADDR_FLASH_SECTOR_4) && (address < ADDR_FLASH_SECTOR_5)) return 0x04;
	if( (address >= ADDR_FLASH_SECTOR_5) && (address < ADDR_FLASH_SECTOR_6)) return 0x05;
	if( (address >= ADDR_FLASH_SECTOR_6) && (address < ADDR_FLASH_SECTOR_7)) return 0x06;
	if( (address >= ADDR_FLASH_SECTOR_7) && (address < ADDR_FLASH_SECTOR_8)) return 0x07;
	if( (address >= ADDR_FLASH_SECTOR_8) && (address < ADDR_FLASH_SECTOR_9)) return 0x08;
	if( (address >= ADDR_FLASH_SECTOR_9) && (address < ADDR_FLASH_SECTOR_10)) return 0x09;
	if( (address >= ADDR_FLASH_SECTOR_10) && (address < ADDR_FLASH_SECTOR_11)) return 0x0A;
	if( (address >= ADDR_FLASH_SECTOR_11) && (address < FLASH_END)) return 0x0B;
	return 0xFF;
}

uint8_t GetFlashSectors(uint32_t address, uint8_t* sectors, uint32_t totalBytes)
{
	uint8_t start_sector = 0;
	uint8_t end_sector = 0;
	uint8_t nbSectors = 0;
	uint32_t end_address = address + totalBytes;

	start_sector = GetSectorNumber(address);
	end_sector = GetSectorNumber(end_address);

	if( ( start_sector == 0xFF ) || ( end_sector == 0xFF ) )
	{
		return 0;
	}

	nbSectors = end_sector - start_sector + 1;

	for(int i = 0; i < nbSectors; i++)
	{
		sectors[i] = start_sector + i;
	}
	return nbSectors;
}


uint8_t CalculateCRC8(uint8_t* data, uint32_t length)
{
    uint8_t crc = 0;

    for(uint32_t i = 0; i < length; i++)
    {
        crc ^= data[i];
        for (int j = 0; j < 8; j++)
        {
            if ((crc & 0x80) != 0)
            {
                crc = (uint8_t)((crc << 1) ^ 0x07);
            }
            else
            {
                crc <<= 1;
            }
        }
    }
    return crc;
}
