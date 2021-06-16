#include <iostream>
#include <vector>
#include <string>
#include <fstream>
#include <string>
#include <ali.hpp>


using namespace std;

int main()
{

    vector<string> msg {"Hello", "C++", "World", "from", "VS Code", "and the C++ extension!"};

    for (const string& word : msg)
    {
        cout << word << " ";
    }
    cout << endl;

    ali a = ali("test.bali");
    a.load();


    system("pause");
}