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
#include <variant>

#include "../lib/messages.hpp"
#include "../lib/network.hpp"

using std::string;
using std::cout;
using std::cin;
using std::ofstream;
using std::ifstream;
using std::get;

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
			
			qry_message query;

			address sender = { 0, .uid.value = 0 };
			address receiver = { 0, .uid.value = 0 };
			
			initialize_header(&query._header, QRY, sender, receiver);
			initialize_qry(&query, CHECK_NETWORK_JOINABILITY);

			Socket sock = new Socket(2044, new string(ip));
			sock << query;
			
			message buffer;
			sock >> buffer;

			if (!msg_is<res_message>(buffer)) 
			{
				// something happened
			}

			res_message res = get<res_message>();
			if (res.success != 1) { 
				switch (res.buffer_a[0]) 
				{
					case 0x00:
						// network full
						break;
				}
			}

			receiver.gasp = res._header.sender.gasp;

			delete query;
			delete res;
			delete buffer;

			ent_message ent;
			initialize_header(&ent._header, ENT, sender, receiver);
			ent.stage = 0;
			
			cout << FORMAT("?????", "?????") << "Enter the network key: ";
			cin >> ent.buffer[0];

			sock << ent;


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

	
}