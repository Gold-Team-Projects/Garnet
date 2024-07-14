#include "network.hpp"
#include "messages.hpp"

//   memcpy(buffer, &value, sizeof(T));
//    memcpy(&output, buffer, sizeof(T));


void print_bytes(byte* buffer, size_t size)
{
    for (size_t i = 0; i < size; ++i) {
        printf("%02X ", static_cast<byte>(buffer[i]));
    }
    printf("\n");
}


message_type msg_typeof(byte* msg) 
{
    return (message_type)msg[0];
}

bool msg_is(byte* msg, message_type type) 
{
    return (message_type)msg[0] == type;
}