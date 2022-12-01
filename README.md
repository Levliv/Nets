# Nets repository
### [ServerApp](GroupChat/ServerChat)
Contains server part of a group chat application.
Listens all connections from any Ip and for the port described in lauching parametrs.
It collects information about all users logged in and save the messages they send.
NB: Don't forget to pass 'port'

### [ClientApp](GroupChat/ClientChat)
Contains client part of an application, interacts with user, sends and receives massages for ip and port described in lauching parametrs.
NB: Don't forget to pass 'ip' and 'port'

## License
Code in `GroupChat` directory is licensed under the [MIT Licence](LICENSE).
