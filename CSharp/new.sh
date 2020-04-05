if [ "$1" = "" ]; then
  echo "No arguments provided"
  exit 1
else
  mkdir "hw/$1"
  cat "./.templates/program.cs" > "hw/$1/$1.cs"
fi
