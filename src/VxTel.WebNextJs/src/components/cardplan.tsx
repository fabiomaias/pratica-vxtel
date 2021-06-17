import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Typography from '@material-ui/core/Typography';
import styles from '../../styles/cardplan.module.css';

export default function CardPlan(props: any) {

  return (
    <Card className={styles.root}>
      <CardActionArea>
        <CardMedia
          className={styles.media}
          image="https://thumbs.dreamstime.com/b/photo-cute-girl-hold-telephone-funny-expression-play-wire-wear-plaid-shirt-retro-headband-isolated-purple-color-photo-cute-202895878.jpg"
          title="Image Call"
        />
        <CardContent>
          <Typography gutterBottom variant="h6" component="h2">
            {props.planName}
          </Typography>
          <Typography variant="body2" color="textSecondary" component="p">
            {props.planMinutes} minutos inclusos
          </Typography>
        </CardContent>
      </CardActionArea>
    </Card>
  );
}