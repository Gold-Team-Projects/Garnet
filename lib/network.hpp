#ifndef NETWORK_H
#define NETWORK_H

#include <stdint.h>
#include <iostream>
#include <string.h>
#include <map>
#include <boost/asio.hpp>
#include <boost/any.hpp>

#include "messages.hpp"

using namespace boost::asio;


void print_bytes(byte* buffer, size_t size);

message_type msg_typeof(byte* msg);
bool msg_is(byte* msg, message_type type);

class Socket 
{
	public:
		Socket(uint16_t port, std::string url)
		{
			this->pointer = this->buffer;
			this->port = port;
			this->url = url;
		}
		void operator<<(byte* data) 
		{
			
		}
		void operator>>(byte* buffer) 
		{
			memcpy(buffer, this->pointer, sizeof(this->buffer));
		}
	private:
		byte		buffer[512];
		byte*		pointer;
		std::string url;
		uint16_t	port;
};

#endif