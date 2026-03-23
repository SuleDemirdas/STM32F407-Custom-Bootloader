/*
 * bootloader.c
 *
 *  Created on: Mar 9, 2026
 *      Author: Şule Nur Demirdaş
 */

#include "bootloader.h"

static UART_Transmit_FuncPtr_t p_UART_Tx_Func = NULL;

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

int WriteMemory(uint32_t address)
{
	return 1;
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
