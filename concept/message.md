# Message Concept
**Messages** are sent between Garnet nodes to communicate.
## Universal Headers

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