#include "messages.hpp"
#include <time.h>
#include <stdio.h>
#include <stdlib.h>
#include <stddef.h>
#include <string.h>
#include <chrono>

using nampespace std;

void initialize_req(req_message* req, req_type type) 
{
	req->subject = (uint8_t)type;
	req->header.message_type = 0x00;
	req->header.message_size = 0xD9;
}