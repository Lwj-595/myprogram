#include <iostream>
#include<stdlib.h>
#include <queue>
#include <stack>
#include <string>
#include <fstream>
using namespace std;
int fff = 0;
string readFileContent(string str) {
	std::ifstream fin(str);
	if (!fin.is_open())
	{
		exit(1);
	}
	string name;
	fin >> name;
	fin.close();
	return name;
}
struct TNode
{
	string c;
	double f;  //出现的频率
	int idx;  //线性表中的位置索引号
	int parents;    //父亲标号
	int l_child;
	int r_child;
	TNode() :parents(-1), l_child(-1), r_child(-1), f(0) {}

};
bool operator>(const TNode &x, const TNode &y)
{
	return x.f > y.f;
}
void printCode( int n,TNode *t)
{   
	int sum222 = 0;
	int mt = 0;
	double xt = 0;
	for (int i = 0; i < n;i++) 
	{   int sum = 0;
		cout << t[i].c<<":";
		int idx = t[i].idx;
			//idx叶子结点索引号
			stack <int> s;
		while (t[idx].parents != -1)
		{
			if (idx == t[t[idx].parents].l_child)
			{
				s.push(0);
			}
			else
			{
				s.push(1);
			}
			idx = t[idx].parents;
		}
		while (!s.empty())
		{
			cout << s.top();
			sum++;
			s.pop();
		}
		mt = t[i].f*sum;
		cout << endl;
		sum222 += mt;
	}
	cout << "哈夫曼压缩率为：" << (((double)sum222 / (fff* 8))*100)  << "%" << endl;
}
int main() 
{
	double f[1000];
	string a;
	a = readFileContent("test.txt");
	char charcode[255];
	int numcode[255];
	for (int i = 0; i < 255; i++)  //ASCII码数目统计数组初始化
	{
		numcode[i] = 0;
	}
	int index = 0;
	for (unsigned int i = 0; i<a.length(); i++)//取出从文件中读出的字符串中的某个字符
	{
		bool flag = true;
		for (int j = 0; j < sizeof(charcode); j++)//和已经存在字符数组中的字符比较，如果已经存在，个数加一，如果不存在，添加到字符数组中
		{
			if (charcode[j] == a[i])
			{
				numcode[j]++;
				flag = false;
			}
		}
		if (flag)
		{
			charcode[index] = a[i];
			numcode[index] = 1;
			index++;
		}
	}
	for (int i = 0; i < index; i++)//输出检索出来的单个字符及其个数
	{
		if (i % 6 == 0) {
			cout << endl;
			cout << charcode[i] << ":" << numcode[i] << "个     ";
		}
		else {
			cout << charcode[i] << ":" << numcode[i] << "个     ";
		}
	}
	
	cout << endl;
	cout <<"以下为哈夫曼编码 "<< endl;
	int n = index;
	TNode *t = new TNode[2*n - 1];
    for (int i = 0; i < n; i++)
	{
			t[i].c = charcode[i];
			t[i].f = numcode[i];
			fff += numcode[i];
			t[i].parents = -1;
			t[i].l_child = -1;
			t[i].r_child = -1;
			t[i].idx = i;
     }
	std::priority_queue<TNode, std::vector<TNode>, std::greater<TNode> >PQ;
	for (int i=0;i<n;i++)
	{
		PQ.push(t[i]);
	}
	int next = n;    //下一个要生成的结点
	while (!PQ.empty())
	{
		TNode L = PQ.top();
		PQ.pop();
		TNode R = PQ.top();
		PQ.pop();
		TNode &P=t[next];
		P.l_child= L.idx;
		P.r_child = R.idx;
		P.f = L.f + R.f;
		P.idx = next;
		t[next] = P;
		t[L.idx].parents = t[R.idx].parents = next;
		PQ.push(P);
		next++;
		if (next ==(2 * n - 1))
		{
			break;
		}
	}
    printCode(n,t);	
	int x;
	cin >> x;
}


