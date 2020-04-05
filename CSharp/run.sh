if [ "$1" = "" ]; then
  echo "No arguments provided"
  exit 1
else
  mcs -out:"./hw/$1/$1.exe" "./hw/$1/$1.cs"

  if [ "$2" = "" ]; then
    mono "./hw/$1/$1.exe"
  else
    cat $2 | mono "./hw/$1/$1.exe"
  fi
fi