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
#include "messages.h"
#include "network.h"

#define VERSION "0.1.0"

using namespace std;
using std::string;

int main(int argc, char *argv[])
{
    argv[0][strlen(argv[0]) - 3] = '\0';
    char* path = argv[0];

    system("clear");
    printf("[GTC] Garnet Terminal Client v%s starting...\n", VERSION);
    
    char* buffer;
    asprintf(&buffer, "%sdata.txt", path);
    ifstream _data(buffer);
    if (_data.fail())
    {
        printf("[GTC] First time detected!\n");
        _data.close();

        asprintf(&buffer, "%sdata.txt", path);
        ofstream data(buffer);

        buffer = "";
        printf("[GTC] Enter a GASP IP: ");
        cin >> buffer;

        data.close();

    }
    else 
    {
        printf("Second");
    }

    return 0;
}
