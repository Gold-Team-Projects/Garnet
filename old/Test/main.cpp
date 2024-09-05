#include <stdio.h>
#include <stddef.h>
#include <stdlib.h>
#include <stdint.h>

#include "messages.hpp"
#include "network.hpp"

int main()
{
	res_message res1;

	address s;
	s.uid.value = 0;
	s.gasp = 0;
	
	address r;
	r.uid.value = 0;
	r.gasp = 0;

	initialize_header(&res1._header, RES, s, r);
	res1.success = 1;

	byte* buffer1;

	memcpy(buffer1, &res1, sizeof(res_message));

	res_message res2; 
	memcpy(&res2, buffer1, sizeof(res_message));
	printf("Success? %i", res2.success);
	print_bytes(buffer1, sizeof(res1));

	return 0;
}