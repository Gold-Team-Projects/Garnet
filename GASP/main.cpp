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
#include <boost/format.hpp>

#include "messages.hpp"
#include "network.hpp"

using std::string;
using std::cout;
using std::cin;
using std::ofstream;
using std::ifstream;
using std::get;
using boost::format;

#define VERSION "0.0.0"
#define FORMAT(x, y) "[GASP " << x << " - " << y << "] "

bool gasp_already_connected(void);
void handle(void);

int main(int argc, char *argv[])
{
	if (gasp_already_connected()) {
		handle();
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
			data << "0\n";
			data << netname << "\n";
			data << /* hash this */ netkey << "\n";
			
			cout << FORMAT("00000", netname) << "Enter the IP of this machine: ";
			char buffer1[16];
			cin >> buffer1;
			data << "0 => " << buffer1 << "\n";

			data.close();

			cout << "Network created!";
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

			if (!msg_is<res_message>(buffer)) { /*something happened*/ }

			res_message res = get<res_message>(buffer);
			if (res.success != 1) { 
				switch (res.buffer_a[0]) 
				{
					case 0x00:
						// network full
						break;
				}
				return;
			}

			receiver.gasp = res._header.sender.gasp;

			delete query;
			delete res;
			delete buffer;

			ent_message ent;
			initialize_header(&ent._header, ENT, sender, receiver);
			ent.stage = 0;
			
			cout << FORMAT("?????", "?????") << "Enter the network key: ";
			cin >> ent.buffer;

			sock << ent;

			message buffer2;
			sock >> buffer2;

			if (!msg_is<res_message>(buffer2)) { /* something happened */ }

			res_message res2 = get<res_message>(buffer);
			if (res2.success != 1) 
			{
				switch (res2.buffer_a[0]) 
				{
					case 0x00:
						// incorrect key
						break;
				}
				return;
			}

			/*
			A - Data
			0: GASP ID
			1: Name size
			B - Name
			C - null
			*/

			uint8_t gasp_id = res2.buffer_a[0];

			char* name;
			for (int i = 0; i < res2.buffer_a[2]; ++i) {
				name[i] = (char)res2.buffer_b[i];
			}

			ofstream data("./data.txt");
			data << gasp_id << '\n';
			data << name << '\n';
			data << ent.buffer << '\n';

			// still have to go through block 0

			sender.gasp = gasp_id;
			byte[512] block;
			int addresses = 0;
			for (int j = 0; j < 128; ++j) {
				qry_message block_query;
				initialize_header(&block_query, QRY, sender, receiver);
				initialize_qry(&query, GET_ADDRESS_BLOCK);
				block_query.buffer_a[0] = j;
				sock << block_query;

				message buffer3;
				sock >> buffer3;

				res_message res3 = get<res_message>(buffer3);
				for (int n = 0; n < 128; ++n) { block[n + 0  ] = res3.buffer_a[n + 0  ]; }
				for (int m = 0; m < 128; ++m) { block[m + 128] = res3.buffer_b[m + 128]; }
				for (int o = 0; o < 256; ++o) { block[o + 256] = res3.buffer_c[o + 256]; }

				delete res3;
				delete buffer3;
				delete block_query;

				for (int k = 0; k < 16; ++k) {
					data << (format("%i => %i%.%i%.%i%.%i%\n") % address 
						% block[k + 0] 
						% block[k + 1] 
						% block[k + 2] 
						% block[k + 3]).str();
					++addresses;
				}
			}
			data.close();
			delete data;
			delete block;
			delete addresses;
			cout << FORMAT(gasp_id, name) << "Successfully connected to the network!";
		}
		handle();
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