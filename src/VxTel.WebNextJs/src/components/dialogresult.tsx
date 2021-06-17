import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import MonetizationOnOutlinedIcon from '@material-ui/icons/MonetizationOnOutlined';
import styles from '../../styles/dialogresult.module.css';

export default function DialogResult(props: any) {

  return (
    <div className={styles.root}>
      <Paper elevation={3} className={styles.green}>
        <MonetizationOnOutlinedIcon className={styles.iconMoney} />
        <Typography variant="h5" component="h3" className={styles.text}>
            Com Plano
        </Typography>
        <Typography className={styles.currency}>
            {props.withPlan.toLocaleString('pt-br',{style: 'currency', currency: 'BRL'})}
        </Typography>
      </Paper>
      <Paper elevation={3} className={styles.red}>
        <MonetizationOnOutlinedIcon className={styles.iconMoney} />
        <Typography variant="h5" component="h3" className={styles.text}>
            Sem Plano
        </Typography>
        <Typography className={styles.currency}>
            {props.withoutPlan.toLocaleString('pt-br',{style: 'currency', currency: 'BRL'})}
        </Typography>
      </Paper>
    </div>
  );
}