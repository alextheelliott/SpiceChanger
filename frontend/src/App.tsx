import React, { useState } from 'react';
import { Button, Card, Stack, ThemeProvider, ToggleButtonGroup, Typography } from '@mui/joy';
import MicIcon from '@mui/icons-material/Mic';

import Spices from './pages/Spices';
import Restock from './pages/Restock';
import Com from './pages/Com';

function App() {
  const [tabState, setTabState] = useState(0);

  return (
      <div className="App">
        <Stack spacing={2} style={{margin:'24px'}}>
          <Card sx={{ borderRadius: 20 }}>
            <Stack spacing={1} direction="row">
              <Button style={{borderRadius:'12px'}} variant={tabState==0?'solid':'soft'} onClick={() => (setTabState(0))}>Spices</Button>
              <Button style={{borderRadius:'12px'}} variant={tabState==1?'solid':'soft'} onClick={() => (setTabState(1))}>Restock</Button>
              <Button style={{borderRadius:'12px'}} variant={tabState==2?'solid':'soft'} onClick={() => (setTabState(2))}>COM</Button>
            </Stack>
          </Card>
          <Card sx={{ borderRadius: 20 }} style={{height:400}}>
            {
              tabState == 0 ?
                <Spices /> :
              tabState == 1 ?
                <Restock /> :
              tabState == 2 ? 
                <Com /> :
                <p>Tab Error</p>
            }
          </Card>
        </Stack>
      </div>
  );
}

export default App;
