#include <stdio.h>
#include <stdlib.h>
#include <stddef.h>
#include <string.h>
#include <iostream>
#include <fstream>
#include <stdint.h>
#include <unistd.h>
#include <netinet/in.h>
#include <sys/socket.h>
#include "messages.hpp"
#include "network.hpp"

#define VERSION "0.1.0"

using namespace std;
using std::string;

int main(int argc, char *argv[])
{
    argv[0][strlen(argv[0]) - 3] = '\0';
    char* path = argv[0];

    system("clear");
    printf("[GTC] Garnet Terminal Client v%s starting...\n", VERSION);
    
    char* buffer1;
    asprintf(&buffer1, "%sdata.txt", path);
    fstream data(buffer1);
    delete buffer1;

    if (data.fail())
    {
        printf("[GTC] First time detected!\n");
        printf("[GTC] Enter a GASP IP: ");

        char*   buffer2;
        cin >>  buffer2;

        Socket sock = new Socket(new string(buffer2), 2046);
        
        
    }
    else 
    {
        printf("Second");
    }

    return 0;
}
