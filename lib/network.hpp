#ifndef NETWORK_H
#define NETWORK_H

#include <stdint.h>
#include <iostream>
#include <boost/asio.hpp>

#include "messages.hpp"

template <typename T>
void to_bytes(const T& value, byte* buffer);
template <typename T>
T from_bytes(byte* buffer);
void print_bytes(byte* buffer, size_t size);

class Socket 
{
	public:
		Socket(uint16_t port) 
		{
			this->pointer = this->buffer;
			this->port = port;
		}
		void operator<<(auto data) 
		{
			to_bytes(data, this->pointer);
			// send buffer
		}
		void operator>>(void* ptr) 
		{
			*ptr = *this->pointer;
		}
	private:
		byte	buffer[512];
		byte*	pointer;
		uint16_t port;
};

#endif