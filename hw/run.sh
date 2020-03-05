if [ "$1" = "" ]; then
  echo "No arguments provided"
  exit 1
else
  mcs -out:"$1/$1.exe" "$1/$1.cs"

  if [ "$2" = "" ]; then
    mono "./$1/$1.exe"
  else
    cat $2 | mono "./$1/$1.exe"
  fi
fi