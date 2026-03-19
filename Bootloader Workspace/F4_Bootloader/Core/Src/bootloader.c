/*
 * bootloader.c
 *
 *  Created on: Mar 9, 2026
 *      Author: Şule Nur Demirdaş
 */

#include "bootloader.h"

int test = 0;
uint8_t response_get_version[2] = {0};
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
uint8_t response_get_help[13] = {0};

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
	uartTransmit(UART_PORT, response_get_version, 2);
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

	uartTransmit(UART_PORT, response_get_help, sizeof(response_get_help));
}

