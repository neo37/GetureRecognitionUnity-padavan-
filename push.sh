
#!/bin/bash

#php database/migrate/dump.php


git status

read -p "Type the comment for commit: " comment
# Check if string is empty using -z. For more 'help test'    
if [[ -z "$comment" ]]; then
   printf '%s\n' "No comment entered"
   exit 1
else
   # If userInput is not empty show what the user typed in and run ls -l
    printf "You entered %s " "$comment"
    git add -A
	git commit -am "$comment"
	git push origin master
fi


read -p "Press any key to exit" key