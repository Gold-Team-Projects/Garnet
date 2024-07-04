#include "messages.hpp"
#include <time.h>
#include <stdio.h>
#include <stdlib.h>
#include <stddef.h>
#include <string.h>
#include <chrono>

using nampespace std;

void initialize_qry(qry_message* query, qry_type type) 
{
	query->subject = (uint8_t)type;
	query->header.message_type = 0x00;
	query->header.message_size = 0xD9;
}