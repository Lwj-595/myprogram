
public class BallAndBall {
	public void ballCrach(Ball b1, Ball b2){
		int x1 =  b1.x + b1.d/2;
		int y1 =  b1.y + b1.d/2;
		int x2 =  b2.x + b2.d/2;
		int y2 =  b2.y + b2.d/2;
		
		double e = Math.sqrt((x1-x2)*(x1-x2) + (y1-y2)*(y1-y2));
		//�����ײ��
		if (e <= b1.d/2 + b2.d/2) {
			//b1С��
			switch (b1.position) {
			case Ball.LEFT_UP:
				b1.position = Ball.RIGHT_DOWN;
				break;
			case Ball.RIGHT_UP:
				b1.position = Ball.LEFT_DOWN;
				break;
			case Ball.LEFT_DOWN:
				b1.position = Ball.RIGHT_UP;
				break;
			case Ball.RIGHT_DOWN:
				b1.position = Ball.LEFT_UP;
				break;
			}
			//b2С��
			switch (b2.position) {
			case Ball.LEFT_UP:
				b2.position = Ball.RIGHT_DOWN;
				break;
			case Ball.RIGHT_UP:
				b2.position = Ball.LEFT_DOWN;
				break;
			case Ball.LEFT_DOWN:
				b2.position = Ball.RIGHT_UP;
				break;
			case Ball.RIGHT_DOWN:
				b2.position = Ball.LEFT_UP;
				break;
			}
		}
	}
	
	//����Ƿ���ײ��
	public boolean isBallCrach(Ball b1, Ball b2){
		boolean flag = false;
		int x1 =  b1.x + b1.d/2;
		int y1 =  b1.y + b1.d/2;
		int x2 =  b2.x + b2.d/2;
		int y2 =  b2.y + b2.d/2;
		//����Բ�ľ�
		double e = Math.sqrt((x1-x2)*(x1-x2) + (y1-y2)*(y1-y2));
		
		if (e <= b1.d/2 + b2.d/2) {
			return true;
		}
		
		return false;
	}
}
