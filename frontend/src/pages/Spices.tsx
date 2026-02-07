import React, { useState } from 'react';
import { Button, Card, Stack } from '@mui/joy';
import MicIcon from '@mui/icons-material/Mic';
import ForwardIcon from '@mui/icons-material/Forward';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';

function Spices() {
  const buttons = [...Array(8)].map((_, i) => {
    const angle = (2 * Math.PI * i) / 8 - Math.PI / 2 + Math.PI / 8;
    const x = (400)/2 + (140) * Math.cos(angle);
    const y = (400)/2 + (140) * Math.sin(angle);

    return (
      <Button
        key={i}
        variant='outlined'
        sx={{
          bgcolor: "neutral.50",
          position: "absolute",
          left: x,
          top: y,
          transform: "translate(-50%, -50%)",
          borderRadius: "50%",
          minWidth: 100,
          height: 100,
          padding: 0,
        }}
      >
        {i + 1}
      </Button>
    );
  });
  
  return (
    <Stack spacing={1} direction="row" sx={{height:'100%',width:'100%'}}>
      <Card sx={{position: "relative",borderRadius:(400)/2}} color="primary" variant="soft" style={{height:(400-32),width:(400-32)}}>
        {buttons}
        <Button
          variant='outlined'
          sx={{
            bgcolor: "neutral.50",
            position: "absolute",
            left: 200,
            top: 200,
            transform: "translate(-50%, -50%)",
            borderRadius: "50%",
            minWidth: 120,
            height: 120,
            padding: 0,
          }}
        >
          <MicIcon fontSize='large'/>
        </Button>
      </Card>
      <Stack spacing={1} sx={{height:'100%'}} justifyContent='space-around' direction='column'>
        <KeyboardDoubleArrowRightIcon color='primary'/>
        <KeyboardDoubleArrowLeftIcon color='primary'/>
      </Stack>
      <Stack spacing={1} sx={{flex: '1 1 0'}} direction='column'>
        <Stack spacing={1} sx={{flexGrow:1}}>
          <Card sx={{height:'100%'}}/>
        </Stack>
        <Stack spacing={1} sx={{flexGrow:1}}>
          <Card sx={{height:'100%'}}/>
        </Stack>
      </Stack>
      <Stack spacing={1} sx={{height:'100%'}} justifyContent='space-around' direction='column'>
        <KeyboardDoubleArrowRightIcon color='primary'/>
        <KeyboardDoubleArrowLeftIcon color='primary'/>
      </Stack>
      <Stack spacing={1} sx={{flex: '1 1 0'}} direction='column'>
        <Stack spacing={1} sx={{flexGrow:1}}>
          <Card sx={{height:'100%'}}/>
          <Button variant='outlined' sx={{height: 20, width:'100%'}} fullWidth>Return</Button>
        </Stack>
      </Stack>
    </Stack>
  );
}

export default Spices;
