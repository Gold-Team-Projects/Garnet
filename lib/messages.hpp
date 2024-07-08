#ifndef MESSAGES_H
#define MESSAGES_H

#include <stdint.h>
#include <stdlib.h>
#include <stddef.h>
#include <variant>

using std::variant;

struct _uint56_t {
	uint64_t value : 56;
} __attribute__((packed));
typedef _uint56_t uint56_t;
typedef uint8_t byte;

struct address {
	uint8_t gasp;
	uint56_t uid;
} __attribute__((packed));
struct ipv4 {
	uint8_t octet1, octet2, octet3, octet4;
} __attribute__((packed));

enum message_type
{
	RES = 0x00,
	QRY = 0x01,
	PNG = 0x02,
	ENT = 0x03
};
struct header {
	uint8_t		message_type;

	address		sender;
	address		receiver;

	uint16_t    year;
	uint8_t     month;
	uint8_t     day;
	uint8_t		hour;
	uint8_t     min;
	uint8_t     sec;
} __attribute__((packed)); // 24 bytes
void initialize_header(header* header, message_type type, address sender, address receiver);
void initialize_time(header* header);


enum query_type 
{
	GET_NEW_ADR					= 0x00,
	GET_ADR_BY_USR				= 0x01,
	GET_USR_BY_ADR				= 0x02,
	CHECK_NETWORK_ACCEPTANCE	= 0x03,
	GET_ADR_BLOCK				= 0x04
};
struct qry_message {
	header		_header;
	uint8_t		subject;
	byte		buffer_a[64];
	byte		buffer_b[64];
	byte		buffer_c[32];
	byte		buffer_d[32];
} __attribute__((packed)); // 218 bytes
void initialize_qry(qry_message* query, query_type type);

struct png_message {
	header	_header;
	byte	padding;
} __attribute__((packed)); // 25 bytes

struct ent_message {
	header		_header;
	uint8_t		stage;
	byte		buffer[128];
} __attribute__((packed)); // 161 bytes

struct res_message {
	header	_header;
	byte	success;
	byte	buffer_a[128];
	byte	buffer_b[128];
	byte	buffer_c[256];
} __attribute__((packed)); //


typedef variant<res_message, qry_message, png_message, ent_message> message;
#endif