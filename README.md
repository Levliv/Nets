# Nets repository
### [ServerApp](GroupChat/ServerChat)
Contains server part of a group chat application.
Listens all connections from any Ip and for the port described in lauching parametrs.
It collects information about all users logged in and save the messages they send. <br> <br>
NB: Don't forget to pass `port` <br>
ex. for localhost use 6789

### [ClientApp](GroupChat/ClientChat)
Contains client part of an application, interacts with user, sends and receives massages for ip and port described in lauching parametrs.

NB: Don't forget to pass `ip` and `port` <br>
ex. for localhost use ::1 6789

## License
Code in `GroupChat` directory is licensed under the [MIT Licence](LICENSE). <br>
