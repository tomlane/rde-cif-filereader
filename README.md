# Network Rail Open Data CIF Schedule File Reader

This project contains a single extension method that can be used on an IEnumerable<string> (such as from reading a file or other collection of CIF lines).

This method will check the type of the record from the first two characters and return a relevent object. 

The base object just contains the CIF record line itself. The other is a "Basic Schedule" which will group the base schedule, the extra details and any child records for that schedule.
