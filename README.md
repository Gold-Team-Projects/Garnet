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

## Example network
In this example network (EXN), there are 32 GSPs and 3072 clients (96 for each server).

### Messaging examples
Client A wants to send the message "Hello" to Client B.
Client A has a GID of `62@0000-0001`. This address contains 2 pieces of information:
- `62`: This is the local identifier, which is used to to find a specific client out of all the ones connected to the target GSP.
- `0000-0001`: This is the global identifier. It it used to find the GSP that the client belongs to.
Client B has a GID of `34@0000-0025`, meaning it is the 34th client belonging to GSP 

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
