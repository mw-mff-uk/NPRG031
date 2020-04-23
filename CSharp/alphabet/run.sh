if [ "$1" = "" ]
then
  echo "Task argument not found"
  exit 1
fi

if [ "$1" = 1 ]
then
  input="task-one.txt"
elif [ "$1" = 2 ]
then
  input="task-two.txt"
else
  echo "Invalid task argument"
  exit 1
fi

dir=$(dirname $0)

mcs "${dir}/alphabet.cs" -out:"${dir}/alphabet.exe"
cat "${dir}/${input}" | mono "${dir}/alphabet.exe" --print-to-console "--task:$1"
rm "${dir}/alphabet.exe"