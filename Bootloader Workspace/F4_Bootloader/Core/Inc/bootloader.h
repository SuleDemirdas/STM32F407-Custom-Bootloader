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
#define CONNECT						0x62

#define ACK							0x79
#define NACK						0x1F
#define UNKNOWN						0x99

#define NUM_OF_COMMANDS				10
#define RESPONSE_GET_HELP_SIZE		13
#define RESPONSE_GET_VERSION_SIZE	2
#define RESPONSE_GET_ID_SIZE		4
#define RESPONSE_READ_MEM_SIZE      257
#define RESPONSE_CONNECT_SIZE	    4
#define WRITE_MEM_BLOCK_SIZE		64
#define MAX_NUM_OF_SECTORS			12
#define SRAM2_END					0x2001FFFF

// --- SEKTÖR BAŞLANGIÇ ADRESLERİ ---
#define ADDR_FLASH_SECTOR_0     ((uint32_t)0x08000000) /* Base @ of Sector 0, 16 Kbytes */
#define ADDR_FLASH_SECTOR_1     ((uint32_t)0x08004000) /* Base @ of Sector 1, 16 Kbytes */
#define ADDR_FLASH_SECTOR_2     ((uint32_t)0x08008000) /* Base @ of Sector 2, 16 Kbytes */
#define ADDR_FLASH_SECTOR_3     ((uint32_t)0x0800C000) /* Base @ of Sector 3, 16 Kbytes */
#define ADDR_FLASH_SECTOR_4     ((uint32_t)0x08010000) /* Base @ of Sector 4, 64 Kbytes */
#define ADDR_FLASH_SECTOR_5     ((uint32_t)0x08020000) /* Base @ of Sector 5, 128 Kbytes */
#define ADDR_FLASH_SECTOR_6     ((uint32_t)0x08040000) /* Base @ of Sector 6, 128 Kbytes */
#define ADDR_FLASH_SECTOR_7     ((uint32_t)0x08060000) /* Base @ of Sector 7, 128 Kbytes */
#define ADDR_FLASH_SECTOR_8     ((uint32_t)0x08080000) /* Base @ of Sector 8, 128 Kbytes */
#define ADDR_FLASH_SECTOR_9     ((uint32_t)0x080A0000) /* Base @ of Sector 9, 128 Kbytes */
#define ADDR_FLASH_SECTOR_10    ((uint32_t)0x080C0000) /* Base @ of Sector 10, 128 Kbytes */
#define ADDR_FLASH_SECTOR_11    ((uint32_t)0x080E0000) /* Base @ of Sector 11, 128 Kbytes */

typedef int (*UART_Transmit_FuncPtr_t)(uint8_t* data, uint32_t size);

void Bootloader_Init(UART_Transmit_FuncPtr_t p_uart_transmit_func);
uint8_t CalculateCRC8(uint8_t* data, uint32_t length);
int EraseFlashSectors(uint8_t sector);
uint8_t GetFlashSectors(uint32_t address, uint8_t* sectors, uint32_t totalBytes);
uint8_t GetSectorNumber(uint32_t address);
void processBootloaderCommand(char* buffer);
void handleGetVersion(void);
void handleGetHelp(void);
void handleGetID(void);
int handleReadMem(char* buffer);
int handleGoToAddress(char* buffer);
void GoToAddress(uint32_t address);
int handleWriteMemory(char* buffer);
int handleErase(char* buffer);
void handleWriteProtectUnprotect(char* buffer);
void handleConnect(void);
void handleReadOutProtectUnprotect(char* buffer);

#endif /* INC_BOOTLOADER_H_ */
