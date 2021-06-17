import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import PhoneInTalkIcon from '@material-ui/icons/PhoneInTalk';
import styles from '../../styles/topbar.module.css';

export default function AppTopBar() {

  return (
    <div className={styles.root}>
      <AppBar position="static">
        <Toolbar className={styles.colorbar}>
          <PhoneInTalkIcon />
          <Typography variant="h6" className={styles.title}>
            VxTel, Aqui Você Fala Mais.
          </Typography>
        </Toolbar>
      </AppBar>
    </div>
  );
}