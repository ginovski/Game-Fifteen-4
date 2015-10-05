# Game-Fifteen-4
Refactoring Documentation for Project “GameFifteen”                                                                                                                         
------------------------------------------------------

1.  Redesigned the project structure: 
	-   Renamed the project to `Game15`.
	-   Renamed the main class `Program` to `GameFifteen`.
	-   Extracted each class in a separate file with a good name: `GameFifteen.cs`, `Player.cs`... more to be extracted!
	- ...
2.  Reformatted the source code using StyleCop:

	-   Removed all unneeded empty lines. For example:
	
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
	-   Inserted empty lines between the methods.
	-   Formatted the curly braces **{** and **}** according to the best practices for the C\# language.
	-   Put **{** and **}** after all conditionals and loops (when missing).
	-   Character casing: variables and fields made **camelCase**
     - Character casing: types and methods made **PascalCase**.
	-   Formatted all other elements of the source code according to the best practices introduced in the course “[High-Quality Programming Code](http://telerikacademy.com/Courses/Courses/Details/244)”.
	-   …
3.  Renamed variables:
	-   ...
4.  Introduced constants:
	-   `GameBoardSize = 16`
5. Renamed Class DvoikaImeRezultat to Player.
     - Changed struct to public class
     - Rename private field score to movesCount and asign this.
