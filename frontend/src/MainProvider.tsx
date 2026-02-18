import React, { createContext, useContext, useState, ReactNode } from 'react';

// --- TYPES ---
interface Spice {
  ind: number;
  name: string;
  state: 'stored' | 'stored2lent' | 'lent' | 'lent2stored';
}

interface ComContextType {
  connected: boolean;
  getComPorts: () => Promise<void>;
  connectPort: (port: string) => Promise<void>;
  disconnectPort: () => Promise<void>;
  writeMessage: (msg: string) => Promise<void>;
}

interface SpicesContextType {
  spices: Spice[];
  setSpices: React.Dispatch<React.SetStateAction<Spice[]>>;
  fetchSpices: () => Promise<void>;
  fetchSpiceDict: () => Promise<void>;
  postSpices: () => Promise<void>;
}

// --- CONTEXT INITIALIZATION ---
const ComContext = createContext<ComContextType | undefined>(undefined);
const SpicesContext = createContext<SpicesContextType | undefined>(undefined);

// --- COM PROVIDER ---
export const ComProvider = ({ children }: { children: ReactNode }) => {
  const [connected, setConnected] = useState(false);

  const getComPorts = async () => {

  }

  const connectPort = async (port: string) => {
    setConnected(true);
  }

  const disconnectPort = async () => {
    setConnected(false);
  }

  const writeMessage = async (msg: string) => {
    // setComState((prev) => ({ ...prev, status: 'loading' }));
    // try {
    //   // Dummy API Call
    //   await new Promise((res) => setTimeout(res, 1000));
    //   setComState({ status: 'idle', lastMessage: msg });
    //   console.log(`API Call (COM): Sent ${msg}`);
    // } catch (e) {
    //   setComState((prev) => ({ ...prev, status: 'error' }));
    // }
  };

  return (
    <ComContext.Provider value={{
      connected,
      getComPorts,
      connectPort,
      disconnectPort,
      writeMessage,
    }}>
      {children}
    </ComContext.Provider>
  );
};

// --- SPICES PROVIDER ---
export const SpicesProvider = ({ children }: { children: ReactNode }) => {
  const [spices, setSpices] = useState<Spice[]>([
    { ind: 1, name: 'Cumin', state: 'stored' },
    { ind: 2, name: 'Cayenne', state: 'stored' },
  ]);

  const fetchSpices = async () => {
    console.log("Fetching spices from hosted API...");
    // Logic for your hosted API fetch would go here
  };

  const fetchSpiceDict = async () => {
    console.log("Fetching spice dict from hosted API...");
    // Logic for your hosted API fetch would go here
  };

  const postSpices = async () => {
    console.log("Posting spices to hosted API...");
    // Logic for your hosted API fetch would go here
  };

  const findEmptyIndex = (): number => {
    return 1;
  };

  return (
    <SpicesContext.Provider value={{ 
      spices,
      setSpices,
      fetchSpices,
      postSpices,
      fetchSpiceDict,
    }}>
      {children}
    </SpicesContext.Provider>
  );
};

// --- CUSTOM HOOKS ---
export const useCom = () => {
  const context = useContext(ComContext);
  if (!context) throw new Error('useCom must be used within a ComProvider');
  return context;
};

export const useSpices = () => {
  const context = useContext(SpicesContext);
  if (!context) throw new Error('useSpices must be used within a SpicesProvider');
  return context;
};

// --- GLOBAL PROVIDER COMPONENT ---
export const GlobalProvider = ({ children }: { children: ReactNode }) => {
  return (
    <ComProvider>
      <SpicesProvider>
        {children}
      </SpicesProvider>
    </ComProvider>
  );
};