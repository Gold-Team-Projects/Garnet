#include "network.hpp"

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