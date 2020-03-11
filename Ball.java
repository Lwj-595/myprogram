import java.awt.Color;
import java.awt.Font;
import java.awt.Graphics;
public class Ball {
     /*����С�������*/
	int x,y;//����С�������
	int d;//С���ֱ��
	Color ballColor;//С�����ɫ
	int speed;//С����˶��ٶ�
	int position;//С����˶�����
	public static final int LEFT_UP = 0;//����
	public static final int RIGHT_UP = 1;//����
	public static final int LEFT_DOWN = 2;//����
	public static final int RIGHT_DOWN = 3;//����
	
	public Ball(int x, int y, int position, int d, int speed, Color ballColor)
	{
		this.x = x;
		this.y = y;
		this.position = position;
		this.d = d;
		this.speed = speed;
		this.ballColor = ballColor;
	}
	//�����
	public Ball(int x, int y, int d, int speed, Color ballColor){
		this.x = x;
		this.y = y;
		this.d = d;
		this.speed = speed;
		this.ballColor = ballColor;
	}
	public void drawBall(Graphics g)
	{
		g.setColor(ballColor);
		g.fillOval(x, y, d, d);
	}
	public void drawBall2(Graphics g)
	{
		g.setColor(ballColor);
		g.fillOval(x, y, d, d);
		g.setColor(Color.RED);
		Font font=new Font(Font.DIALOG,Font.BOLD,14);
        g.setFont(font);
        g.drawString("QAQ", x+d/2, y+d/2);
	}
	//�˶�
	public void ballMove()
	{
		switch(this.position)
		{
		case LEFT_UP:
			x-=speed;
			y-=speed;
			if(x<=0)
			{
				this.position=RIGHT_UP;
			}else if(y<=0)
			{
				this.position=LEFT_DOWN;
			}
		  break;
		case RIGHT_UP:
			x += speed;
			y -= speed;
			if (x >= BallMain.SCREEN_WIDTH - d) {
				this.position = LEFT_UP;
			}else if (y <= 0) {
				this.position = RIGHT_DOWN;
			}
			break;
		case LEFT_DOWN:
			x -= speed;
			y += speed;
			if (x <= 0) {
				this.position = RIGHT_DOWN;
			}else if (y >= BallMain.SCREEN_HEIGHT - d) {
				this.position = LEFT_UP;
			}
			break;
		case RIGHT_DOWN:
			x += speed;
			y += speed;
			if (x >= BallMain.SCREEN_WIDTH - d) {
				this.position = LEFT_DOWN;
			}else if (y >= BallMain.SCREEN_HEIGHT - d) {
				this.position = RIGHT_UP;
			}
			break;
			}
		}
}

