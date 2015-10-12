# Game-Fifteen-4
Refactoring Documentation for Project вЂњGameFifteenвЂќ                                                                                                                         
------------------------------------------------------

1.  Redesigned the project structure: 
	-   Renamed the project to `Game15`.
	-   Renamed the main class `Program` to `GameFifteen`.
	-   Extracted each class in a separate file with a good name: `GameFifteen.cs`, `Player.cs`... more to be extracted!
	- ...
2.  Reformatted the source code using StyleCop:

	-   Removed all unneeded empty lines and added proper spacing. For example:
	
	Before:
	
	    if (matrix[nextCellRow,nextCellColumn] == cellNumber.ToString())
                    
                    {
                    
                        direction = dir;
                        break;
                    }
		
	After:

	    if (matrix[nextCellRow, nextCellColumn] == cellNumber.ToString())
                    {
                        direction = dir;
                        break;
                    }
    - Inserted empty lines after statements and elements wrapped in curly brackets and used **string.Empty** rather than "".
    
    	Before:
	
		     using (topWriter)
                {
                    topWriter.Write("");
                }
                return new string[TopScoresAmount];
                    
         After:
         
              using (topWriter)
                {
                    topWriter.Write("");
                }
                
                return new string[TopScoresAmount];
                
	- Inserted empty lines between the methods.
    - Placed all using directives inside of their namespaces.
    - Removed unnecessary parenthesis.
       
    	Before:
	
		      bool isRowValid = (nextCellRow >= 0 && nextCellRow < MatrixSizeRows);
         After:
         
               bool isRowValid = nextCellRow >= 0 && nextCellRow < MatrixSizeRows;
                
    -  Added access modifiers for all the classes.
	-   Formatted the curly braces **{** and **}** according to the best practices for the C\# language.
	-   Put **{** and **}** after all conditionals and loops (when missing).
	-   Character casing: variables and fields made **camelCase**
     - Character casing: types and methods made **PascalCase**.
	-   Formatted all other elements of the source code according to the best practices introduced in the course вЂњ[High-Quality Programming Code](http://telerikacademy.com/Courses/Courses/Details/244)вЂќ.
	-   вЂ¦
3.  Renamed variables:
4. Renamed methods:
    -   proverka() to IsNextCellValid()
    -   proverka2() to AreNumbersSequential()
4.  Introduced constants:
	-   `GameBoardSize = 16`
	-    'MaxPlayersInScoreboard = 5' etc.
5. Renamed Class DvoikaImeRezultat to Player.
     - Changed struct to public class
     - Rename private field score to movesCount and asign this.
6. DRY principle- extracted methods where needed
7. Dependency injection
8. SOLID- Single responsibility, Open/close, Liskov substitution, Interface segregation, Dependency inversion
7. Design patterns:
   - Structural patterns - facade etc...
   - Creational patterns - singleton etc...
   - Behavioral patterns - command etc...
