/*
 * bootloader.h
 *
 *  Created on: Mar 9, 2026
 *      Author: Şule Nur Demirdaş
 */

#ifndef INC_BOOTLOADER_H_
#define INC_BOOTLOADER_H_

#include "stm32f4xx_hal.h"
#include <stdint.h>

#define BOOTLOADER_HEADER			0x7F
#define APPLICATION_HEADER			0x7E
#define BOOTLOADER_VERSION			0x10
#define DEVICE_ID					(DBGMCU->IDCODE & 0xFF)	// Device's ID(STM32F407x): 0x413
/*COMMANDS*/
#define GET_HELP					0x00
#define GET_VERSION					0x01
#define GET_ID						0x02
#define READ_MEMORY					0x11
#define GO_TO_ADDRESS				0x21
#define WRITE_MEMORY				0x31
#define ERASE						0x43
#define WRITE_PROTECT_UNPROTECT		0x63
#define READOUT_PROTECT_UNPROTECT 	0x82
#define GET_CHECKSUM				0xA1

#define ACK							0x79
#define NACK						0x1F
#define UNKNOWN						0x99

#define NUM_OF_COMMANDS				10
#define RESPONSE_GET_HELP_SIZE		13
#define RESPONSE_GET_VERSION_SIZE	2
#define RESPONSE_GET_ID_SIZE		4
#define RESPONSE_READ_MEM_SIZE      257
#define WRITE_MEM_BLOCK_SIZE		64

#define SRAM2_END					0x2001FFFF

typedef int (*UART_Transmit_FuncPtr_t)(uint8_t* data, uint32_t size);

void processBootloaderCommand(char* buffer);
void handleGetVersion(void);
void handleGetHelp(void);
void handleGetID(void);
int handleReadMem(char* buffer);
int handleGoToAddress(char* buffer);
void GoToAddress(uint32_t address);
int handleWriteMemory(char* buffer);
void Bootloader_Init(UART_Transmit_FuncPtr_t p_uart_transmit_func);
uint8_t CalculateCRC8(uint8_t* data, uint32_t length);



#endif /* INC_BOOTLOADER_H_ */
