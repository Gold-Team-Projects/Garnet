#include <stdio.h>
#include <stdint.h>
#include <string.h>

typedef unsigned char byte;

struct test {
    uint8_t a, b, c;
} __attribute__((packed));

template<typename Dest, typename Src>
Dest to_bytes(const Src* ptr) 
{
    Dest dest;
    printf("memcpy(%i, %i, %i)\n", &dest, ptr, sizeof * ptr)
    memcpy(&dest, ptr, sizeof *ptr);
    return dest;
}

int main() 
{
    char a, b, c;

    a = 'a'; // 61
    b = 'b'; // 62
    c = 'c'; // 63

    printf("a, b, and c as hexes: %02x %02x %02x\n", a, b, c);

    char *ptr;
    char ar[3] = { a, b, c };
    ptr = ar;

    printf("pointer/array test: %c %c %c\n", ptr[0], ptr[1], ptr[2]);

    byte buffer1[3];
    uint8_t buffer2[3];
    test t;
    t.a = 61;
    t.b = 62;
    t.c = 63;

    buffer1[0] = (byte)t.a;
    buffer1[1] = (byte)t.b;
    buffer1[2] = (byte)t.c;

    buffer2[0] = (uint8_t)buffer1[0];
    buffer2[1] = (uint8_t)buffer1[1];
    buffer2[2] = (uint8_t)buffer1[2];

    printf("conversion (as bytes): %c %c %c\n", buffer1[0], buffer1[1], buffer1[2]);
    printf("conversion (as numbers): %d %d %d\n", buffer2[0], buffer2[1], buffer2[2]);
    printf("addresses: %hhn %hhn %hhn\n", &t.a, &t.b, &t.c);
    printf("sizeof(t): %li\n", sizeof(t));

    byte* buffer3;
    buffer3 = to_bytes<byte*, test>(&t);

    printf("buffer3: %i %i %i", buffer3[0], buffer3[1], buffer3[2]);
}