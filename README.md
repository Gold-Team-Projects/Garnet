# Garnet
Garnet is a decentralized CDN-WAN hybrid chat network.

## Idea
Garnet is, at its core, a protocol to connect multple clients for communication purposes.
Garnet combines the idea of Content Delivery Networks (which use multiple servers to deliver content),
and Wide-Area Networks (which are networks that span across huge distances).
Garnet is decentralized, meaning that it does not rely on a single set of servers.
It uses multiple GSPs to process data.

Garnet uses a P2P Chord network of servers to transfer messages.
Each server is connected to a number of clients.

## Components
### GSPs
Garnet Service Providers are the servers for any Garnet network.

## Example network
In this example network (EXN), there are 32 GSPs and 3072 clients (96 for each server).

### The high subnet
The high subnet is the subnet of GSPs. 
It's a chord P2P network consisting of up to 256 nodes.
Each GSP handles clients in a specific geological zone which spans with an area of 100<sup>2</sup> miles.

### The low subnet
The low subnet is the network of clients.
Clients normally communicate through GSPs and the high subnet, but in some cases direct communication is needed.
This network has not specific shape.

### How messages are passed
In the network, each GSP has a unique ID from 0 to 255.
GSPs have a list of 8 other GSPs and their IP addresses. This is called a routing table.

This is an example table for a GSP with the ID 8.
| Index | GUID | IP |
| ----- | ---- | -- |
| 0 | 9 | 0.0.0.0 |
| 1 | 10 | 0.0.0.1 |
| 2 | 12 | 0.0.0.2 |
| 3 | 16 | 0.0.0.3 |
| 4 | 24 | 0.0.0.4 |
| 5 | 40 | 0.0.0.5 |
| 6 | 72 | 0.0.0.6 |
| 7 | 136 | 0.0.0.7 |

In this example, we will say that the sender (user A) has an id of 68@64.
User B (the receiver) has an id of 14@136.
User A's client will send the message to GSP 64.
GSP 64's closest entry in it's routing table is GSP 128, to it forwards the message there.
GSP 128 has GSP 136 in its routing table, so it sends the message directly to GSP 136.
GSP 136 then sends the message to client 14 in its user lookup table.



## Security
Garnet uses TLS to ensure safe communication between nodes. 
It also has end-to-end encryption..

## Terms
### Node 
A node is a single part of a network, either a client or a server.
### Connection
A connection is a link between two nodes in a network.
This can be client-to-client, server-to-server or client-to-server.
### Network
A network is a group of connected nodes that work together to send messages.
### GSN
The GSN (or Garnet Standard Network) is the main network of the Garnet protocol.
### GTC & GSC
The Garnet Terminal Client and the Garnet Standard Client are premade clients for everyday use.
### GSP
*G*arnet *S*ervice *P*roviders are the servers that make up a network. 
They handle message routing and other server-side mechanics.
### Concepts
Concepts are featuress that aren't standardized, and may or may not be supported depending on the network.
