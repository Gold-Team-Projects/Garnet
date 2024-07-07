# Message Concept
**Messages** are sent between Garnet nodes to communicate.
## Message Types
| Type		| Hex	|
|-----------|-------|
| RES		| 0x00	|
| QRY		| 0x01	|
| PNG		| 0x02	|
| ENT		| 0x03	|
## Universal Header
 ```c
 struct header {
	uint8_t		message_type; // type of message

	address		sender; // sender's garnet address
	address		receiver; // receiver's garnet address

	uint16_t    year; // year sent
	uint8_t     month; // month sent
	uint8_t     day; // day sent
	uint8_t		hour; // hour sent
	uint8_t     min; // minute sent
	uint8_t     sec; // second sent
} __attribute__((packed));
 ```
 ## Messages
 ### RES
 ```c
 struct res_message {
	header	_header; // header
	byte	success; // signifies success
	byte	buffer_a[128]; // buffer for data
	byte	buffer_b[128]; // buffer for data
	byte	buffer_c[256]; // buffer for data
} __attribute__((packed));
 ```
 RES messages respond to other messages.
 ### QRY
 ```c
 struct qry_message {
	header		_header; // header
	uint8_t		subject; // query_type 
	byte		buffer_a[64]; // buffer for data
	byte		buffer_b[64]; // buffer for data
	byte		buffer_c[32]; // buffer for data
	byte		buffer_d[32]; // buffer for data
} __attribute__((packed));
 ```
QRY messages are used to get information from a node.