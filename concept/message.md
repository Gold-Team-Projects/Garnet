# Message Concept
**Messages** are sent from client to client to communicate.
## Universal Headers
```c
struct universal_header {
    uint16_t    s_gasp;      // Sender GASP
    uint48_t    s_uid;      // Sender Unique ID
    uint32_t    r_gasp;      // Receiver GASP
    uint16_t    r_uid;      // Receiver Unique ID
    uint16_t    year;       // Year sent
    uint8_t     month;      // Month sent
    uint8_t     day;        // Day sent
    uint8_t     min;        // Minute sent
    uint8_t     sec;        // Second sent
} __attribute__((packed));
```
## SND 
SND messages send content from one address to another.
### Headers
```c
struct snd_header {
    uint32_t    msg_size;   // Message size in bytes
} __attribute__((packed));
```
## PNG
PNG messages are sent to test response times between 2 addresses.
## RES
RES messages are the standard response to any other type of message. They signify that a message was recieved.
## UPD
UPD messages update a client on a connections context.
## ENT
Asks GASPs to join networks.
## REQ
Asks to get somthing.