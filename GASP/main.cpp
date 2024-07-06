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

#include "../lib/messages.hpp"
#include "../lib/network.hpp"

using std::string;
using std::cout;
using std::cin;
using std::ofstream;
using std::ifstream;

#define VERSION "0.0.0"
#define FORMAT(x, y) "[GASP " << x << " - " << y << "] "

bool gasp_already_connected(void);
void handle(void);

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

			char ip[16];
			cin >> ip;

			byte buffer[512];
			
			qry_message query;

			address sender;
			address receiver;
			sender.gasp = 0;
			sender.uid.value = 0;
			receiver.gasp = 0;
			receiver.uid.value = 0;
			
			initialize_header(&query._header, QRY, sender, receiver);

		}
	}
}

bool gasp_already_connected(void) {
	ifstream data("data.txt");
	if (data.fail()) { return false; }
	return true;
}

void handle(void) 
{
	/*
	 * Port 2044: GASP Network Communications (General Polls, network joins, etc)
	 * Port 2045: GASP Command Communications (Forwarding, specific polls, etc)
	 * Port 2046: Client Communications (Addresses, routing, receiving stored messages)
	 */
	
	Socket port_2044 = new Socket(2044);
	Socket port_2045 = new Socket(2045);
	Socket port_2046 = new Socket(2046);

	
}