#include "messages.hpp"
#include <time.h>
#include <stdio.h>
#include <stdlib.h>
#include <stddef.h>
#include <string.h>
#include <stdint.h>
#include <ctime>

void initialize_qry(qry_message* query, query_type type) 
{
	query->subject = (uint8_t)type;
}

void initialize_header(header* header, message_type type, address sender, address receiver)
{
	header->message_type = (uint8_t)type;
	header->sender = sender;
	header->receiver = receiver;
	
}

void initialize_time(header* header) 
{
	time_t current_time; 
	time(&current_time);

	tm* real_time = localtime(&current_time);

	header->day = real_time->tm_mday;
	header->month = real_time->tm_mon;
	header->year = real_time->tm_year;

	header->hour = real_time->tm_hour;
	header->min = real_time->tm_min;
	header->sec = real_time->tm_sec;
}