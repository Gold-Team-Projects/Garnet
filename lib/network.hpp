#ifndef NETWORK_H
#define NETWORK_H

#include <stdint.h>
#include <iostream>
#include <string.h>
#include <map>
#include <boost/asio.hpp>

#include "messages.hpp"

template <typename T>
void to_bytes(const T& value, byte* buffer);

template <typename T>
T from_bytes(byte* buffer);

void print_bytes(byte* buffer, size_t size);

message transform(byte* buffer);
message_type msg_typeof(message msg);

template <typename T>
bool msg_is<T>(message msg);

class Socket 
{
	public:
		Socket(uint16_t port, std::string url) 
		{
			this->pointer = this->buffer;
			this->port = port;
			this->url = url;
		}
		void operator<<(void data) 
		{
			to_bytes(data, this->pointer);
			// send buffer
		}
		message operator>>() 
		{
			return transform(pointer);
		}
	private:
		byte	buffer[512];
		byte*	pointer;
		std::string url;
		uint16_t port;
};

#endif