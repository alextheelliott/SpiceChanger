import React, { useState } from 'react';
import { Button, Card, Stack, ThemeProvider, ToggleButtonGroup, Typography } from '@mui/joy';
import MicIcon from '@mui/icons-material/Mic';

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
    <Stack spacing={1} direction="row">
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
          <MicIcon/>
        </Button>
      </Card>
    </Stack>
  )

}

export default Spices;
