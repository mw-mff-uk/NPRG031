if [ "$1" = "" ]; then
  echo "No arguments provided"
  exit 1
else
  mcs -out:"./projects/$1/$1.exe" "./projects/$1/$1.cs"

  if [ "$2" = "" ]; then
    mono "./projects/$1/$1.exe"
  else
    cat $2 | mono "./projects/$1/$1.exe"
  fi
fi