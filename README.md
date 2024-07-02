# Garnet-Network
A CDN-WAN hybrid chat network.
## Concept
**Garnet** is a hybrid chat network modeled after the internet. It relies on a **C**ontent **D**elivery **N**etwork (also called a CDN) of 
**G**arnet **A**ddress and **S**ervice **P**roviders (also called GASPs). 

### Scenario
Bob and Alice want to communicate on Garnet. Bob and Alice will both install the Garnet client of their choice. The clients will send requests for addresses
from the nearest GASPs (GASP `31892` for Bob, GASP `773` for Alice) in the **G**arnet **G**lobal **S**tandard **N**etwork. Bob recieved address `78593958@31892`,
and Alice got `3394855777@773`. Alice registers under username `alice101` and Bob registered with `b0b9`. Alice has two ways to ways to contact bob:
#### Direct channels
Alice's client askes the GASP 773 what IP `b0b9` has. GASP 773 polls the GGSN and GASP 31892 forwards the address (which was stored in the address
provision phase) to GASP 773 tells Alice's client and the client directly sends messages to Bob.
#### Routed channels
Alice's client askes GASP 773 to send the message "Hi Bob!" to the user `b0b9`. GASP 773 polls the GGSN for the user `b0b9`, and after finding the address,
uses GASP 31892 to forward messages to `b0b9`.

## Implementation
Garnet is a network in which any third party client can connect. The Garnet Standard Client will be coming soon.

The GASP software is available in the `GASP` folder.

## Contributing
Creating pull requests or issues are great ways to help us!

If you have suggestions or you want to join the team, feel free to contact Jewels directly at `jewels86@proton.me`.
