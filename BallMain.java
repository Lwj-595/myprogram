import java.awt.Dimension;
import java.awt.Toolkit;

import javax.swing.JFrame;

public class BallMain  extends JFrame{
	/**
	 * 
	 */
	private static final long serialVersionUID = -2675960050732273172L;
	//����Ŀ��
		public static final int SCREEN_WIDTH = 1360;
		public static final int SCREEN_HEIGHT = 760;
		
		//ȫ��
	    Dimension d = Toolkit.getDefaultToolkit().getScreenSize();
	    int width = (int)d.getWidth();
	    int height = (int)d.getHeight();
		
		public BallMain(){
			this.setTitle("V1.0");
			//����λ��
			this.setBounds(0,0,SCREEN_WIDTH,SCREEN_HEIGHT);
			
			//���С�򵽴���
			BallJPanel bj = new BallJPanel();
			this.add(bj);
			
			//��Ӽ��̵ļ����¼�
			this.addKeyListener(bj);
			this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
			this.setVisible(true);	
		}
		

		public static void main(String[] args) {
			BallMain b = new BallMain();
			
		}
}
