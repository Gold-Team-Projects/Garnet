#ifndef NETWORK_H
#define NETWORK_H

#include "messages.hpp"

class Sender 
{
	public:

		byte operator <<(message msg) 
		{
			initialize_time(&msg._header);
			byte array[sizeof(msg)] = (byte[])msg;
			return 0;
		}

	private:

};

#endif