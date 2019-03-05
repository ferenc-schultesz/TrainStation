# TrainStation
### Exercise
Develop a search algorithm for a train station ticket machine to help the user entering the name of a station. (Actual code for the UI is not required).
The machine has a touchscreen display which works as follows. As the user types each character of the station’s name on the touchscreen, the display should update to show all valid choices for the next character and a list of possible matching stations (in alphabetical order).

### Acceptance Criteria
* Code quality and maintainability
  * SOLID design principles
  * Appropriate testing
  * Project structure
  * Appropriate data structures
  * Naming
* A complete solution, fulfilling all requirements
* Good runtime performance (memory vs execution speed)

### Restrictions
* The system will be used in a single ticket machine by a single user at a time (there is no need to support concurrent usage).
* Assume that interactive performance is important, i.e. the list of candidate stations must be returned as quickly as possible as the user presses each key.
* Assume that your system may spend as long booting up as needed, so the list of stations may be transformed into a representation which is better suited to servicing the interactive response time requirements.

### Solution
The search algorithm was implemented with *Trie search tree* data structure.  Each `Node` includes  
* `Value` which is a single character
* `Parent` reference to its parent
* `Prefix` that is the sum of the prefix of the parent and the value of the node
* `Children` list of its children

The data structure provides very fast search for partial or full word searches, since for a given input (m) the algorithm only has to visit m nodes, hence the time complexity is O(m).
The initialisation however takes longer, since for each insert, the algorithm first has to check if nodes with similar prefixes already exist rather than just add the full station to a list. 
To compare the performance difference in initialisation and search, the requirements were implemented with a `List` and *Linq* too.
On average the initialisation took about 20 times for the *Trie*, however the search takes about 10 times less. For a software where the time that initialisation takes is not important (applications that don’t change often), the *Trie* provides better results.

Since UI was not part of the requirements, only a basic console interface was implemented where the user can input a string, the application lists the next letters and the train station suggestions and finally the results of the performance test.

![Alt text](/Images/console.png)
