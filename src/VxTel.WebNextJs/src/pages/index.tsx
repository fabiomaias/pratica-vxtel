import React, { useState } from 'react';
import Head from 'next/head';
import Image from 'next/image';
import styles from '../../styles/Home.module.css'
import api from '../services/service-api'
import AppTopBar from '../components/topbar';
import Grid, { GridSpacing } from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import CardPlan from '../components/cardplan';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import { PersonPinCircleSharp } from '@material-ui/icons';
import DialogResult from '../components/dialogresult';
import Typography from '@material-ui/core/Typography';

interface planData {
  id: string,
  name: string,
  minutes: number
}

interface priceData {
  id: string,
  origin: string,
  destination: string,
  charge: number
}

export default function Home({ plans, prices }: any) {
  const [open, setOpen] = useState(false);
  const [originDestination, setOriginDestination] = useState({
    origin: "",
    destination: ""
  });
  const [minutes, setMinutes] = useState(0);
  const [planId, setPlanId] = useState('');
  const [planChosen, setPlanChosen] = useState(''); 
  const [estimateWithPlan, setEstimateWithPlan] = useState(0); 
  const [estimateWithoutPlan, setEstimateWithoutPlan] = useState(0);
  const [errMessages, setErrMessages] = useState<string[]>([]);


  const handleChange = (event: React.ChangeEvent<{name?: string, value: unknown }>) => {
    const value = event.target.value as string;
    setOriginDestination({
      ...originDestination,
      [event.target.name]: value
    });
  };

  const handleClickOpen = () => {
    setOpen(true);
    console.log(planId);
  };

  const handleClose = () =>  {
    setOpen(false);
    setEstimateWithPlan(0);
    setEstimateWithoutPlan(0);
    setErrMessages([]);
  }

  const handleEstimate = () => {
    setEstimateWithPlan(0);
    setEstimateWithoutPlan(0);
    setErrMessages([]);
    const apiEstimate = api.post("/api/estimate-prices", {
      origin: originDestination.origin,
      destination: originDestination.destination,
      time: Number(minutes),
      planId: planId
    })
    .then(response => {
      setEstimateWithPlan(response.data.priceWithPlan);
      setEstimateWithoutPlan(response.data.priceWithoutPlan);
    })
    .catch(err => {
      var arr = [];
      if(err.response.data.Message != null) {
        arr.push(err.response.data.Message);
        //console.log(arr);
      } else{
        var obj = err.response.data.errors;
        var results = Object.keys(obj).map((key) => (obj[key]));
        for(var result of results){
          for(var message of result){
             arr.push(message);
          }
        }
      }
      setErrMessages(arr);
    });
  }

  return (
      <div className={styles.container}>
        <div className={styles.topLogo}>
          <Image src="https://cdn.vortx.com.br/images/logo-expanded-dourado.svg" width="80" height="60"></Image>                
        </div>
        <AppTopBar />
        <Head>
          <title>VxTel</title>
          <meta name="description" content="Calculando os benefícios de ter um plano conosco." />
          <link rel="icon" href="/favicon.ico" />
        </Head>

        <main className={styles.main}>
          <h3 className={styles.title}>
            Escolha um plano e simule o valor das suas chamadas
          </h3>

          <Grid item xs={12}>
            <Grid container justify="center" spacing={2}>
              {
                plans.map((plan: planData) =>
                  <Grid key={plan.id} item>
                    <Button variant="outlined" onClick={() => { handleClickOpen(); setPlanId(plan.id); setPlanChosen(plan.name); }} className={styles.buttonhome}>
                      <CardPlan planName={plan.name} planMinutes={plan.minutes} />
                    </Button>
                  </Grid>)
              }
            </Grid>
          </Grid>
        </main>
        
        <Dialog
          fullWidth
          maxWidth="sm"
          open={open}
          onClose={handleClose}
          className={styles.description}
          aria-labelledby="form-dialog-title">
          <DialogTitle id="form-dialog-title">Calcular Chamada no plano {planChosen}</DialogTitle>
          <DialogContent>
            <DialogContentText>
              Para calcular, informe o ddd de origem, destino e o tempo da ligação.
            </DialogContentText>
            <FormControl className={styles.formControl}>
              <InputLabel id="origin-label">DDD de Origem</InputLabel>
              <Select
                required
                name="origin"
                value={originDestination.origin}
                onChange={handleChange}
              >
              {
                [...new Map(prices.map(item =>
                  [item['origin'], item])).values()].map((price: priceData) => 
                    <MenuItem value={price.origin}>{price.origin}</MenuItem>
                )
              }
              </Select>
            </FormControl>

            <FormControl className={styles.formControl}>
              <InputLabel id="destination-label">DDD de Destino</InputLabel>
              <Select
                required
                name="destination"
                value={originDestination.destination}
                onChange={handleChange}
              >
              {
                [...new Map(prices.map(item =>
                  [item['destination'], item])).values()].map((price: priceData) => 
                  <MenuItem value={price.destination}>{price.destination}</MenuItem>
                )
              }
              </Select>
            </FormControl>
            <TextField
              required
              margin="dense"
              id="minutes"
              label="Tempo da chamada(min)"
              type="number"
              value={minutes}
              onChange={e => setMinutes(e.target.value)}
              className={styles.formCall}
            />
          </DialogContent>
          {errMessages.map(item => <Typography className={styles.errMessages}>{item}</Typography>)}
          {
          estimateWithPlan > 0 || estimateWithoutPlan > 0 ?
            <DialogResult withPlan={estimateWithPlan} withoutPlan={estimateWithoutPlan}/>
            :
          null
          } 
          <DialogActions>
            <Button onClick={handleClose} color="secondary">
              Fechar
            </Button>
            <Button onClick={handleEstimate} color="primary">
              Calcular
            </Button>
          </DialogActions>
        </Dialog>
      </div>
  )
}

export async function getServerSideProps() {
  const apiPlans = await api.get("api/plan");
  const apiPrices = await api.get("api/price");

  return {
    props: {
      plans: apiPlans.data,
      prices: apiPrices.data
    }
  }
}
