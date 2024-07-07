#include "network.hpp"
#include "messages.hpp"
#include <variant>

using std::holds_alternative;

template <typename T>
void to_bytes(const T& value, byte* buffer)
{
    memcpy(buffer, &value, sizeof(T));
}

template <typename T>
T from_bytes(byte* buffer)
{
    T output;
    memcpy(&output, buffer, sizeof(T));
    return output;
}

void print_bytes(byte* buffer, size_t size)
{
    for (size_t i = 0; i < size; ++i) {
        printf("%02X ", static_cast<byte>(buffer[i]));
    }
    printf("\n");
}

message transform(byte* buffer) 
{
    message output = new message();
    message_type type = (message_type)buffer[0];
    switch (type)
    {
        case RES:
            output = from_bytes<res_message>(buffer);
            break;
        case QRY:
            output = from_bytes<qry_message>(buffer);
            break;
        case PNG:
            output = from_bytes<png_message>(buffer);
            break;
        case ENT:
            output = from_bytes<ent_message>(buffer);
            break;
    }

    return output;
}

message_type msg_typeof(message msg) 
{
    return (message_type)msg.index();
}

template <typename T>
bool msg_is<T>(message msg) 
{
    return holds_alternative<T>(msg);
}