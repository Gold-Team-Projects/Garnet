#ifndef MESSAGES_H
#define MESSAGES_H

#include <stdint.h>

struct _uint48_t {
	uint64_t x : 48;
} __attribute__((packed));
struct _uint24_t {
	uint32_t x : 24;
} __attribute__((packed));
typedef _uint48_t uint48_t;
typedef _uint24_t uint24_t;
typedef uint8_t byte;


struct header {
	uint16_t	s_gasp;
	uint48_t	s_uid;
	uint16_t	r_gasp;
	uint48_t	s_uid;

	uint16_t    year;
	uint8_t     month;
	uint8_t     day;
	uint8_t     min;
	uint8_t     sec;

	uint8_t		message_type;
	uint24_t	message_size;
} __attribute__((packed)); // 24 bytes

enum class req_type 
{
	// GASPs
	address = 0x00,

};
struct req_message {
	header		header;
	uint8_t		subject;
	byte		buffer_a[64];
	byte		buffer_b[64];
	byte		buffer_c[32];
	byte		buffer_d[32];
} __attribute__((packed)); // 217 bytes
void initialize_req(req_message* req, req_type type);

struct png_message {
	header	header;
	byte	padding;
} __attribute__((packed)); // 25 bytes

#endif