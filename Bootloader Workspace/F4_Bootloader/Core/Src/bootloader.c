/*
 * bootloader.c
 *
 *  Created on: Mar 9, 2026
 *      Author: Şule Nur Demirdaş
 */

#include "bootloader.h"

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
	uartTransmit(UART_PORT, response_get_version, RESPONSE_GET_VERSION_SIZE);
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

	uartTransmit(UART_PORT, response_get_help, RESPONSE_GET_HELP_SIZE);
}

void handleGetID(void)
{
	response_get_id[0] = ACK;
	response_get_id[1] = 1;
	response_get_id[2] = 0x04;
	response_get_id[3] = DEVICE_ID;

	uartTransmit(UART_PORT, response_get_id, RESPONSE_GET_ID_SIZE);

}

int handleReadMem(char* buffer)
{
	uint8_t addrBytes[4] = {buffer[3],buffer[4],buffer[5],buffer[6]};
	uint8_t crc_received = buffer[7];
	uint8_t numBytesToRead = buffer[8];

	uint8_t crc_calculated = {0};
	uint32_t address = (((uint32_t)buffer[3] << 24) | ((uint32_t)buffer[4] << 16) | ((uint32_t)buffer[5] << 8)  | ((uint32_t)buffer[6]));
	uint8_t* mem_ptr = (uint8_t*)address;

	crc_calculated = CalculateCRC8(addrBytes, 4);

	if (crc_received != crc_calculated)
	{
		response_read_mem[0] = NACK;
		uartTransmit(UART_PORT, response_read_mem, 1);
		return -1;
	}

	if( !((address >= FLASH_BASE && address <= FLASH_END ) || (address >= SRAM1_BASE && address <= SRAM2_END )) )
	{
		response_read_mem[0] = NACK;
		uartTransmit(UART_PORT, response_read_mem, 1);
		return -1;
	}
	response_read_mem[0] = ACK;

	for(int i = 0; i <= numBytesToRead; i++ )
	{
		response_read_mem[i+1] = mem_ptr[i];
	}
	uartTransmit(UART_PORT, response_read_mem, (numBytesToRead + 1));

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
