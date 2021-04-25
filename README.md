# NetworkLogParseManager
A log parser system that deals concurrently with large network log information with the following features:

* Three threads managing "Limited Concurrent Queue" concurrently to consume the network async data logs, parse it and fill the output streamming file. The "Limited Concurrent Queue" is a data structure that cannot have more elements then what is defined in its threshold, avoiding memory overload.

* "Log Line" is composed by n "Log fields", that can be of type expression or not.

* A LogLineParser have several element parsers that will be interchanged during the parsing according to how is mapped the output line.

* Design Patterns -> Strategy, Builder and Abstract Factory
