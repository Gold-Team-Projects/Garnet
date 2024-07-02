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

using namespace std;
using std::string;
using libsocket::inet_stream;

#define VERSION "0.0.0"
#define FORMAT(x, y) "[GASP " << x << " - " << y << "] "

bool gasp_already_connected(void);

int main(int argc, char *argv[])
{
	if (gasp_already_connected()) {
		cout << "Connected";
	}
	else {
		cout << FORMAT("?????", "?????") << "Not connected to any networks.\n";
		cout << FORMAT("?????", "?????") << "Am I the first (y/n)? ";

		char buffer;
		cin >> buffer;
		
		if (buffer == 'Y' || buffer == 'y') {
			cout << FORMAT("00000", "?????") << "Enter a network name: ";

			char netname[32];
			cin >> netname;

			cout << FORMAT("00000", netname) << "Enter activation key: ";

			char netkey[128];
			cin >> netkey;

			cout << FORMAT("00000", netname) << "Setting up GASP...\n";

			ofstream data("./data.txt");
			data << "00000\n";
			data << netname << "\n";
			data << /* hash this */ netkey << "\n";
			
			cout << FORMAT("00000", netname) << "Enter the IP of this machine: ";
			char buffer1[16];
			cin >> buffer1;
			data << "00000 => " << buffer1 << "\n";

			data.close();

			cout << "Network created!";

			handle();
		}
		else {
			cout << FORMAT("?????", "?????") << "Enter an IP of any connected GASP: ";
			char[16] ip;
			cin >> ip;

			string buffer;

			libsocket::inet_stream sock(ip, "2044", LIBSOCKET_IPv4);
			
			sock << "ENT 0";
			sock >> buffer;

			if (buffer == "STP 1") { cout << "Network is full."; }

			// buffer is RES 1
			cout << FORMAT("?????", "?????") << "Enter activation key: ";
			string answer;
			cin >> answer;

			sock << "ENT 1 " << answer;
			sock >> buffer;
			if (buffer[4] != '1') { /*fail*/ }

		}
	}
}

bool gasp_already_connected(void) {
	ifstream s_adrs("server-addresses");
	if (s_adrs.fail()) { return false; }
	return true;
}

void handle() 
{
	
}