#include<stdlib.h>
#include<iostream>
#define N 100
#define Infinity 65535
using namespace std;
struct Matrix //矩阵
{
	int row;   //矩阵行数
	int col;    //矩阵列数
};
//矩阵
Matrix matrix[N];
//m[i][j]存储Ai到Aj的最小乘法次数
int m[N][N];
//s[i][j]存储Ai到Aj之间加括号的位置
int s[N][N];
//打印加括号后的
void print_it(int i, int j)
{
	if (i == j)
		cout << "A" << i;
	else
	{
		cout << "(";
		print_it(i, s[i][j]);
		print_it(s[i][j] + 1, j);
		cout << ")";
	}
}
int main()
{
	int cases;
	cout << "请输入执行次数：" << endl;
	cin >> cases;
	while (cases--)
	{
		//变量初始化
		memset(m, 0, sizeof(m));
		memset(s, 0, sizeof(s));
		cout << "请输入矩阵的个数：" << endl;
		int n;
		cin >> n;
		//flag表示输入是否符合要求
		bool flag = true;
		int i = 1;
		while (flag)
		{
			cout << "请输入每个矩阵行数与列数（注意下一个的矩阵行数要和上一个输入的矩阵列数相同）：" << endl;
			for (i = 1; i <= n; i++)
			{
				cout << "A" << i <<" "<< "行数：";
				cin >> matrix[i].row;
				cout << "A" << i <<" "<<"列数：";
				cin >> matrix[i].col;
			}
			//检查Ai的列数是否等于Ai+1的行数
			for (i = 1; i<n; i++)
			{
				if (matrix[i].col != matrix[i + 1].row)
					break;
			}
			if (i >= n)
				flag = false;
			else
			{
				flag = true;
				cout << "输入不符合要求，存在矩阵的列数不等于后面一个矩阵的行数，请重新输入！" << endl;
			}
		}
		int l, j, k;
		for (l = 2; l <= n; l++)
		{
			for (i = 1; i <= n - l + 1; i++)
			{
				j = i + l - 1;
				m[i][j] =Infinity;   //矩阵链乘积最大乘法次数
				int q = 0;
				for (k = i; k <= j - 1; k++)
				{
					q = m[i][k] + m[k + 1][j] + matrix[i].row*matrix[k].col*matrix[j].col;
					if (m[i][j]>q)
					{
						m[i][j] = q;
						s[i][j] = k;
					}
				}
			}
		}
		cout << "加括号的方式为：" << endl;
		print_it(1, n);
		cout << endl;
		cout << "最小乘法次数为：" << m[1][n] << endl;;
	}
	system("pause");
	return 0;
}