#include<stdlib.h>
#include<iostream>
#define N 100
#define Infinity 65535
using namespace std;
struct Matrix //����
{
	int row;   //��������
	int col;    //��������
};
//����
Matrix matrix[N];
//m[i][j]�洢Ai��Aj����С�˷�����
int m[N][N];
//s[i][j]�洢Ai��Aj֮������ŵ�λ��
int s[N][N];
//��ӡ�����ź��
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
	cout << "������ִ�д�����" << endl;
	cin >> cases;
	while (cases--)
	{
		//������ʼ��
		memset(m, 0, sizeof(m));
		memset(s, 0, sizeof(s));
		cout << "���������ĸ�����" << endl;
		int n;
		cin >> n;
		//flag��ʾ�����Ƿ����Ҫ��
		bool flag = true;
		int i = 1;
		while (flag)
		{
			cout << "������ÿ������������������ע����һ���ľ�������Ҫ����һ������ľ���������ͬ����" << endl;
			for (i = 1; i <= n; i++)
			{
				cout << "A" << i <<" "<< "������";
				cin >> matrix[i].row;
				cout << "A" << i <<" "<<"������";
				cin >> matrix[i].col;
			}
			//���Ai�������Ƿ����Ai+1������
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
				cout << "���벻����Ҫ�󣬴��ھ�������������ں���һ����������������������룡" << endl;
			}
		}
		int l, j, k;
		for (l = 2; l <= n; l++)
		{
			for (i = 1; i <= n - l + 1; i++)
			{
				j = i + l - 1;
				m[i][j] =Infinity;   //�������˻����˷�����
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
		cout << "�����ŵķ�ʽΪ��" << endl;
		print_it(1, n);
		cout << endl;
		cout << "��С�˷�����Ϊ��" << m[1][n] << endl;;
	}
	system("pause");
	return 0;
}